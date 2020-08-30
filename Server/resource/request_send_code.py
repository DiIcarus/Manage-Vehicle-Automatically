from flask_restful import Resource,request
from helper.db import qlnx as db
from uuid import uuid4
import re
import json
from flask_jwt_extended import jwt_required
from helper.utils.time_support import convertString2Timestamp,getTimeStameNow
class Response:
  def __init__(self,status,message,share_key):
    self.status = status,
    self.message = message
    self.share_key = share_key

class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message
def validateData(vehicle_id):
  result = db.selectTable(
    "SELECT * FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\'"
  )
  if result == []:
    return Validate(status=False,message="vehicle_id not found")
  else:
      return Validate(status=True,message="Success")
def validateInput(vehicle_id):
  
  def checkInRange(data,fnum,lnum):
    return len(data)<fnum and len(data)>lnum

  if checkInRange(vehicle_id,0,10):
    return Validate(status=False,message="password range incorrectly")
  return Validate(status=True,message="no error")
def deleteAvailableSendCode(vehicle_id):
  db.selectTable("DELETE FROM vehicles_sharing_counter WHERE id_vehicles=\'"+vehicle_id+"\' AND status=0")
class ApiRequestSendCode(Resource):
  def post(self):
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      vehicle_id = json_demo["vehicle_id"]
    except:
      vehicle_id = request.form['vehicle_id']
    duration = 30*60
    validate = validateInput( vehicle_id=vehicle_id)
    if validate.status:
      validate = validateData(vehicle_id=vehicle_id)
      share_key = str(uuid4())
      if validate.status:
        deleteAvailableSendCode(vehicle_id=vehicle_id)
        owner_id = db.selectTable("SELECT id_owner FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\'")[0][0]
        db.insertSharingCounter(owner_id=owner_id,send_code=share_key,name="unknown")
        db.insertVehiclesSharingCounter(id_vehicles=vehicle_id,send_code=share_key,end_date=getTimeStameNow()+duration,status=0)
        #send mail
        return Response(status=200,message=validate.message,share_key=share_key).__dict__
      else:
        return Response(status=400,message=validate.message,share_key=share_key).__dict__  
    else:
      return Response(status=400,message=validate.message,share_key="").__dict__