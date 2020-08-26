from flask_restful import Resource,request
from helper.db import qlnx as db
from uuid import uuid4
import datetime
from flask_mail import Mail, Message
from flask_jwt_extended import jwt_required


def splitTime(time):
  t = str(time).split(" ")
  year=t[0].split('-')[0]
  month=t[0].split('-')[1]
  day=t[0].split('-')[2]
  hour=t[1].split(':')[0]
  minus=t[1].split(':')[1]
  second=t[1].split(':')[2].split('.')[0]
  return {
    year:year,
    month:month,
    day:day,
    hour:hour,
    minus:minus,
    second:second
  }
def isDateOnMonth(date):
  current_time = splitTime(datetime.datetime.now())
  ticket_time = splitTime(date)
  if current_time.month == ticket_time.month:
    return True
  else:
    return False

def check_id_vehicle_exist(id_vehicle):
  data = db.selectTable(
    querystr="SELECT * FROM id_vehicles WHERE vehicle_id="+id_vehicle
  )
  if len(data) ==0:
    return {status:False,message:"vehicle not found"}
  else:
    return {status:True, message:"vehicle found"}
def check_vehicle_ticket_available(id_vehicle):
  data = db.selectTable(
    querystr="SELECT * FROM tickets WHERE vehicle_id="+id_vehicle
  )
  if len(data) ==0:
    return {status:False,message:"ticket is unavailable"}
  else:
    if isDateOnMonth(data.date):
      return {status:True,message:"ticket is available"}
    else:
      return {status:True,message:"ticket is out of month"}

def getEmailOwnerFromId(vehicle_id):
  result = db.selectTable("SELECT gmail FROM users WHERE id_users=(SELECT user_id FROM Owners WHERE id_owners=(SELECT id_owner FROM vehicle WHERE id_vehicles=${vehicle_id}))")
  return result

class ApiCheckOutVehicleID(Resource):
  def post(self):
    '''
      Using to detect data(vehicle_id, ticket)
    '''
    vehicle_id = request.form['vehicle_id']
    check_id_vehicle = check_id_vehicle_exist(vehicle_id)
    if check_id_vehicle.status:
      return {
        "code":201,
        "data":{
          "exist":False,
          "ticket_available":check_id_vehicle.status,
          "message": check_id_vehicle.message
        }
      }
    else:
      check_ticket = check_vehicle_ticket_available(id_vehicle)
      if check_ticket.status:
        return {
          "code":201,
          "data":{
            "exist":False,
            "ticket_available":check_ticket.status,
            "message":check_ticket.message
          }
        }
      else:
        return {
          "code":201,
          "data":{
            "exist":False,
            "ticket_available":check_ticket.status,
            "message":check_ticket.message
          }
        }
# class ApiCheckOutClientSendSMS:
#   pass
# class ApiCheckOutBotSendSMS:
#   pass
class ApiCheckOutBotSendCodeMail(Resource):
  def __init__(self,app,mail):
    self.app = app
    self.mail = mail
  def get(self):
    vehicle_id = request.form['vehicle_id']
    email = getEmailOwnerFromId(vehicle_id)
    message = Message(
        subject="This is subject",
        sender=self.app.config.get("MAIL_USERNAME"),
        recipients=[email],
        body="Flask QLNX send mail 1234"
    )
    self.mail.send(message)
    return {
      "data":{
        "status":200,
        "message":"mail is sent",
      }
    }
    return 200

def validateCheckout(private_key,public_key,send_code,own_send_code, vehicle_id,user_id,owner_id):
  '''
    check own_send_code => owner checked 
    check public_key + private_key => user without phone
    check public_key + send_code => friend can use the vehicle.
    check password + send_code => for new user.
    if all true => response 200 pass the check
    else => responce 200 fail to achieve the check
  '''
  if len(owner_id)!=0:
    return {
      code:1,
      check_out_type:"owner_id",
    }
  #check public,private key
  def getOwnerKeyFromVehicle(vehicle_id):
    user_id, private_code, public_code = db.selectTable(
      "SELECT user_ids, private_code, public_code OWNERS WHERE ID_OWNERS=(SELECT ID_OWNER FROM VEHICLES WHERE ID_VEHICLES={vehicle_id})"
    )
    return user_id, private_code,public_code
  if len(private_key)!=0 and len(public_key)!=0:
    _user_id, _private_code, _public_code = getOwnerKeyFromVehicle(vehicle_id=vehicle_id)
    if(user_id==_user_id) and private_key==_private_code and public_key == _public_code:
      return True
    else: 
      return False
  #check public_key + send_code
  if len(public_key)!=0 and len(send_code)!=0:
    pass


  


class ApiCheckoutIncomeKey(Resource):
  # @jwt_required
  def post(seft):
    vehicle_id = request.form['vehicle_id']
    user_id = request.form['user_id']
    owner_id = request.form['owner_id']

    private_key = request.form['private_key']
    public_key = request.form['public_key']
    send_code = request.form['send_code']
    own_send_code = request.form['own_send_code']
    validate_result = validateCheckout(private_key, public_key, send_code, own_send_code, vehicle_id, user_id, owner_id)
    if validate_result:

      return 200
    return 200
