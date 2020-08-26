from flask_restful import Resource,request
import datetime
from helper.db import qlnx as db
from flask_jwt_extended import jwt_required
import json

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

def validateInput(vehicle_id,time_duration):
  if len(vehicle_id)<0 or len(time_duration)<0:
    return Validate(status=False,message=validate_message.get("Length_Empty"))
  if len(vehicle_id) >45:
    return Validate(status=False,message=validate_message.get("Length_Long"))
  return Validate(status=True,message=validate_message.get("Success"))

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
class ApiRegisterMonthTicket(Resource):
  #receive vehicle id, time_stamp => register tickets
  @jwt_required
  def post(self):
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      vehicle_id = json_demo["vehicle_id"]
      time_duration = json_demo["time_duration"]
    except:
      vehicle_id = request.form['vehicle_id']
      time_duration = request.form['time_duration']
    selectChekingData()
    date = datetime.datetime.now()
    validate = validateInput(vehicle_id=vehicle_id,time_duration=time_duration)
    if validate.status:
      validate = validateData(vehicle_id=vehicle_id)
      if validate.status:
        db.insertTicket(
          vehicle_id=vehicle_id,
          date=date
        )
        selectChekingData()
        return Response(
          status=200,
          message=validate.message,
          end_date_time=time_duration,
          id_tickets="",
          id_vehicle=vehicle_id,
          ).__dict__
      else:
        return Response(
        status=400,
        message=validate.message,
        end_date_time=time_duration,
        id_tickets="",
        id_vehicle=vehicle_id,
        ).__dict__
    else:
      return Response(
        status=400,
        message=validate.message,
        end_date_time=time_duration,
        id_tickets="",
        id_vehicle=vehicle_id,
        ).__dict__