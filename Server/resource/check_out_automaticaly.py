from flask_restful import Resource,request
import datetime
from helper.db import qlnx as db
from flask_jwt_extended import jwt_required
import json
from helper.utils.time_support import convertString2Timestamp,getTimeStameNow

validate_message={
  "Length_Empty" : 'Wrong Input length',
  "Mail_Input_Format_Wrong" : 'Gmail input format wrong',
  "Length_Long" : "Input length so long",
  "Success":"No error",
  "Vehicle_Not_Found":"Vehicle not found",
  "Wrong_Password":"Wrong password"
}

class Response:
  def __init__(self,status,message,end_date_time,id_tickets,id_vehicle):
    self.status=status
    self.message=message
    self.end_date_time = end_date_time
    self.id_tickets=id_tickets
    self.id_vehicle=id_vehicle

class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message

def validateInput(vehicle_id,share_code):
  def checkInRange(data,fnum,lnum):
    return len(data)<fnum and len(data)>lnum

  if checkInRange(vehicle_id,0,10):
    return Validate(status=False,message="vehicle_id range incorrectly")
  if checkInRange(share_code,0,10):
    return Validate(status=False,message="share_code range incorrectly")
  return Validate(status=True,message="no error")

def validateData(vehicle_id):
  result = db.selectTable(
    "SELECT * FROM VEHICLES WHERE ID_VEHICLES=\'"+vehicle_id+"\'"
  )
  if result == []:
    return Validate(status=False,message=validate_message.get("Vehicle_Not_Found"))
  else:
    return Validate(status=True,message=validate_message.get("Success"))

def selectChekingData():
  table_ticket = db.selectTable(
    "SELECT * FROM Tickets "
  )
  print(table_ticket)
  table_vehicle = db.selectTable(
    "SELECT * FROM VEHICLES "
  )
  print(table_vehicle)

def checkSendCode(key_code,vehicle_id):
  result_send_code = db.selectTable("SELECT send_code,end_date FROM vehicles_sharing_counter WHERE id_vehicles=\'"+vehicle_id+"\' AND status=0")
  if(result_send_code==[]):
    return Validate(status=True,message="Success")
  else:
    return Validate(status=False,message="Key invalid")

def checkKey(share_code,vehicle_id):
  owner_id = db.selectTable("SELECT id_owner FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\'")[0]
  result = db.selectTable("SELECT private_code,public_code FROM owners WHERE id_owners=\'"+owner_id+"\'")
  if result==[]:
    return Validate(status=False,message="Key invalid")
  else:
    private_code,public_code = result[0]
    if(public_code==share_code):
      validate = checkSendCode(key_code=share_code,vehicle_id=vehicle_id)
      if(private_code==key_code):
        return Validate(status=True,message="Success")
      elif(validate.status):
        return validate
      else:
        return validate
    else:
      return Validate(status=False,message="Key invalid")

def checkTicketAvailable(vehicle_id):
  ticket = db.selectTable(
    "SELECT dates,duration FROM tickets WHERE vehicle_id=\'"+vehicle_id+"\'"
  )
  if ticket == []:
    return Validate(status=False,message="Ticket unavailable")
  else:
    for t in ticket:
      date,duration = t
      if((date+duration)>getTimeStameNow()):
        return Validate(status=True,message="Ticket available")
    return Validate(status=False,message="Ticket unavailable")

class ApiCheckOutTypeInput(Resource):
  #receive vehicle id, time_stamp => register tickets
  @jwt_required
  def post(self):
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      vehicle_id = json_demo["vehicle_id"]
      share_code = json_demo["share_code"]
    except:
      vehicle_id = request.form['vehicle_id']
      share_code = request.form['share_code']
    date = getTimeStameNow()
    validate = validateInput(vehicle_id=vehicle_id,share_code=share_code)
    if validate.status:
      validate = validateData(vehicle_id=vehicle_id)
      if validate.status:
        validate = checkTicketAvailable(vehicle_id=vehicle_id)
        if validate.status:
          validate = checkKey(share_code=share_code,vehicle_id=vehicle_id)
          if validate.status:
            key_code = db.selectTable("SELECT private_code FROM owners WHERE id_owners=(SELECT id_owner FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\')")[0]
            db.insertCheckOut(
              vehicle_id=vehicle_id,
              key_code=key_code,
              share_code=share_code
              date=date
            )
            return Response(
                status=201,
                message=validate.message,
                end_date_time=date + duration,
                id_tickets="",
                id_vehicle=vehicle_id,
              ).__dict__
          else:
            return Response(
            status=400,
            message=validate.message,
            end_date_time=date + duration,
            id_tickets="",
            id_vehicle=vehicle_id,
          ).__dict__
        else:
          return Response(
            status=400,
            message=validate.message,
            end_date_time=date + duration,
            id_tickets="",
            id_vehicle=vehicle_id,
          ).__dict__
      else:
        return Response(
        status=400,
        message=validate.message,
        end_date_time=date + duration,
        id_tickets="",
        id_vehicle=vehicle_id,
        ).__dict__
    else:
      return Response(
        status=400,
        message=validate.message,
        end_date_time=date + duration,
        id_tickets="",
        id_vehicle=vehicle_id,
        ).__dict__