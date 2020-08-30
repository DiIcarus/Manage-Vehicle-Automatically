from flask_restful import Resource,request
from helper.db import qlnx as db
from uuid import uuid4
import re
import json
from flask_jwt_extended import jwt_required
from helper.utils.time_support import convertString2Timestamp
class Response:
  def __init__(self,status,message):
    self.status = status,
    self.message = message

class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message
def validateData(vehicle_id,user_id):
  resultUserId = db.selectTable(
    "SELECT * FROM Users WHERE id_users=\'"+user_id+"\'"
  )
  resultVehicle = db.selectTable(
    "SELECT * FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\'"
  )
  stringQuery = "SELECT * FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\' AND id_owner=(SELECT id_owners FROM owners where user_id=\'"+user_id+"\')"
  resultUserVehicle = db.selectTable(stringQuery)
  if resultUserId == []:
    return Validate(status=False,message="User not found")
  else:
    if resultVehicle !=[]:
      return Validate(status=False,message="Vehicle id exist")
    else:
      if resultUserVehicle !=[]:
        return Validate(status=False,message="Vehicle ID Registed")
      return Validate(status=True,message="Success")

def validateInput(vehicle_id,user_id):
  
  def checkInRange(data,fnum,lnum):
    return len(data)<fnum and len(data)>lnum

  if checkInRange(vehicle_id,0,10):
    return Validate(status=False,message="vehicle_id range incorrectly")
  if checkInRange(user_id,0,10):
    return Validate(status=False,message="user_id range incorrectly")
  return Validate(status=True,message="no error")

class ApiRegisterVehicle(Resource):
  @jwt_required
  def post(self):
    vehicle_id = request.form['vehicle_id']
    user_id = request.form['user_ids']
    validate = validateInput( vehicle_id=vehicle_id, user_id=user_id)
    if validate.status:
      validate = validateData(vehicle_id=vehicle_id,user_id=user_id)
      if validate.status:
        owner_id = db.selectTable("SELECT id_owners FROM Owners WHERE user_id=\'"+user_id+"\'")[0][0]
        db.RegisterVehicle(
          vehicle_id=vehicle_id,
          owner_id=owner_id,
        )
        print(db.selectTable(("SELECT * from users")))
        return Response(status=200,message=validate.message).__dict__
      else:
        return Response(status=400,message=validate.message).__dict__  
    else:
      return Response(status=400,message=validate.message).__dict__