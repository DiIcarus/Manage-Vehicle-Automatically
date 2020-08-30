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
def validateData(gmail,phone_number):
  resultGmail = db.selectTable(
    "SELECT * FROM Users WHERE gmail=\'"+gmail+"\'"
  )
  resultPhoneNumber = db.selectTable(
    "SELECT * FROM Users WHERE phone_number=\'"+phone_number+"\'"
  )
  if resultGmail != []:
    return Validate(status=False,message="Gmail exist")
  else:
    if resultPhoneNumber !=[]:
      return Validate(status=False,message="Phone number exist")
    else:
      return Validate(status=True,message="Success")
def validateInput(gmail,phone_number,dob,password,name):
  
  def checkInRange(data,fnum,lnum):
    if len(data)>fnum and len(data)<lnum:
      return False
    else:
      return True


  if checkInRange(password,0,10):
    return Validate(status=False,message="password range incorrectly")
  if checkInRange(phone_number,0,10):
    return Validate(status=False,message="phone_number range incorrectly")
  if checkInRange(name,0,10):
    return Validate(status=False,message="name range incorrectly")
  if checkInRange(gmail,0,45):
    return Validate(status=False,message="incorrect type of email")
  if not re.match(r"^[A-Za-z0-9\.\+_-]+@[A-Za-z0-9\._-]+\.[a-zA-Z]*$", gmail):
    return Validate(status=False,message="incorrect type of email")
  return Validate(status=True,message="no error")

class ApiRegister(Resource):
  def post(self):
    gmail = request.form['gmail']
    phone_number = request.form['phone_number']
    dob = request.form['dob']
    password = request.form['password']
    name = request.form['name']
    validate = validateInput( gmail=gmail, phone_number=phone_number, dob=dob, password=password, name=name)
    if validate.status:
      validate = validateData(gmail=gmail,phone_number=phone_number)
      if validate.status:
        db.Register(
          gmail=gmail,
          phone_number=phone_number,
          dob= convertString2Timestamp(dob),
          password=password,
          name=name,
        )
        print(db.selectTable(("SELECT * from users")))
        return Response(status=201,message=validate.message).__dict__
      else:
        return Response(status=400,message=validate.message).__dict__  
    else:
      return Response(status=400,message=validate.message).__dict__