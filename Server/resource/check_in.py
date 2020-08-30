from flask_restful import Resource,request
from uuid import uuid4
from helper.db import qlnx as db
from uuid import uuid4
import re
import datetime
from helper.utils.time_support import getTimeStameNow
import json
class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message
def randomIdString():
  return str(uuid4()).split('-')[0]

def check_id_vehicle_exist(id_vehicle):
  data = db.selectTable(
    querystr="SELECT * FROM id_vehicles WHERE vehicle_id="+id_vehicle
  )
  if len(data) ==0:
    return False
  else:
    return True

def registerNewOwner(gmail,phone_number,password,id_vehicle):
  """
  return {id_users,id_owner}
  """
  id_users = randomIdString()
  name = "unknown"
  id_owners= randomIdString()

  db.insertUser(
    id_users=id_users,
    gmail=gmail,
    phone_number=phone_number,
    dob=getTimeStameNow(),
    password=password,
    name=name
  )
  db.insertOwner(
    id_owners=id_owners,
    user_id=id_users,
    private_code=phone_number,
    public_code=password
  )
  db.insertVehicle(
    id_vehicles=id_vehicle,
    id_owner=id_owners
  )
  return id_users,id_owners

def check_vehicle_ticket_available(id_vehicle):
  data = db.selectTable(
    querystr="SELECT * FROM tickets WHERE vehicle_id="+id_vehicle
  )
  if len(data) ==0:
    return False
  else:
    return True
class ApiCheckInWithBot(Resource):
  def post(self):
    id_vehicle= request.form["id_vehicle"]
    if check_id_vehicle_exist():
      ticket_available=check_vehicle_ticket_available(id_vehicle)
      return {
        "code":201,
        "data":{
          "exist":True,
          "ticket_available":ticket_available,
          "message": "id_vehicle is available" if ticket_available else "id_vehicle is unavailable"
        }
      }
    else:
      return{
        "code":201,
        "data":{
          "exist":False,
          "ticket_available":False,
          "message":"id_vehicle not found"
        }
      }

def validateData(password,phone_number,gmail,vehicle_id):
  
  def checkInRange(data,fnum,lnum):
    if len(data)<fnum and len(data)>lnum:
      return True
    else:
      return False

  if checkInRange(password,0,10):
    return Validate(status=False,message="password range incorrectly")
  if checkInRange(phone_number,0,10):
    return Validate(status=False,message="phone_number range incorrectly")
  if checkInRange(vehicle_id,0,10):
    return Validate(status=False,message="vehicle_id range incorrectly")
  if checkInRange(gmail,0,45):
    domain = re.search("@[\w.]+", gmail)
    if len(domain)>0:
      return Validate(status=True,message="no error")
    else:
      return Validate(status=False,message="incorrect type of email")
  return Validate(status=True,message="no error")

class ApiCheckInInsertData(Resource):
  def post(self):
    '''
    user insert password and phone_number/gmail.
    then we create new owner.
    private_id = sdt.
    public_id  = password
    '''
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      vehicle_id = json_demo["vehicle_id"]
      password = json_demo["password"]
      phone_number = json_demo['phone_number']
      gmail = json_demo['gmail']
    except:
      vehicle_id = request.form['vehicle_id']
      password = request.form['password']
      phone_number = request.form['phone_number']
      gmail = request.form['gmail']
    err_code = validateData(
      password=password,
      phone_number=phone_number,
      gmail=gmail,
      vehicle_id=vehicle_id
    )
    if err_code.status is False:
      return {
        "status":400,
        "message":err_code.message,
        "vehicle_id":vehicle_id
      }
    arr = db.selectTable(
      "SELECT * FROM VEHICLES WHERE ID_VEHICLES=\'"+vehicle_id+"\'"
    )
    if len(arr)==0:
      id_users,id_owners = registerNewOwner(
        gmail=gmail,
        phone_number=phone_number,
        password=password,
        id_vehicle=vehicle_id
      )
    db.insertCheckIn(
    vehicle_id=vehicle_id,
    key_code=phone_number,
    dates=getTimeStameNow()
    )
    return {
      "status":201,
      "message":"checked in",
      "vehicle_id":vehicle_id,
    }