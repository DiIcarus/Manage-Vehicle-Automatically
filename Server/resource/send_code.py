from flask_restful import Resource,request
from helper.db import qlnx as db
from uuid import uuid4
import re
import json
from flask_jwt_extended import jwt_required
from helper.utils.time_support import convertString2Timestamp,getTimeStameNow
class Response:
  def __init__(self,status,message):
    self.status = status,
    self.message = message

class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message
    
def validateData(send_code,vehicle_id):
  findSendCode = db.selectTable(
    "SELECT * FROM Vehicles_sharing_counter WHERE send_code=\'"+send_code+"\' AND id_vehicles=\'"+vehicle_id+"\'"
  )
  # stringQuery = "SELECT * FROM vehicles WHERE id_vehicle\'"+vehicle_id+"\' AND id_owner=(SELECT id_owners FROM owners WHERE user_id=\'"+user_id+"\')"
  # resultUserVehicle = db.selectTable(stringQuery)
  if findSendCode == []:
    return Validate(status=False,message="Send code not found")
  else:
    _,_,status,end_date=findSendCode[0]
    if(end_date<getTimeStameNow()):
      return Validate(status=False,message="Send code out of date")
    else:
      if status ==1:
        return Validate(status=False,message="Send code check fail")
      else:
        return Validate(status=True,message="Success")

def validateInput(send_code,vehicle_id):
  
  def checkInRange(data,fnum,lnum):
    return len(data)<fnum and len(data)>lnum

  if checkInRange(send_code,0,10):
    return Validate(status=False,message="send_code range incorrectly")
  if checkInRange(vehicle_id,0,10):
    return Validate(status=False,message="vehicle_id range incorrectly")
  return Validate(status=True,message="no error")

class SendCode(Resource):
  def post(self):
    send_code = request.form['send_code']
    vehicle_id = request.form['vehicle_id']
    validate = validateInput( send_code=send_code, vehicle_id=vehicle_id)
    if validate.status:
      validate = validateData(send_code=send_code,vehicle_id=vehicle_id)
      if validate.status:
        db.updateSendCode(
          vehicle_id=vehicle_id,
          send_code=send_code,
        )
        private_code = db.selectTable("SELECT private_code FROM Owners WHERE id_owners=(SELECT id_owner FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\')")[0][0]
        db.insertCheckOut(
          vehicle_id=vehicle_id,
          key_code=private_code,
          share_code=send_code,
          dates=str(getTimeStameNow())
        )
        print(db.selectTable(("SELECT * from vehicles_sharing_counter")))
        return Response(status=200,message=validate.message).__dict__
      else:
        return Response(status=400,message=validate.message).__dict__  
    else:
      return Response(status=400,message=validate.message).__dict__