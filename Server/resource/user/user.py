from flask_restful import Resource,request
from helper.db import qlnx as db
from werkzeug.security import safe_str_cmp
from flask_jwt_extended import (
    create_access_token, 
    create_refresh_token, 
    jwt_required, 
    jwt_refresh_token_required, 
    get_jwt_identity, 
    get_raw_jwt
  )
import json
import re
from datetime import datetime
from helper.utils.time_support import convertString2Timestamp,getTimeStameNow
import uuid
# def validateInput():
class UserInfo:
  def __init__(self,id_user,gmail,phone_number,dob,password,name):
    self.id_user=id_user
    self.gmail=gmail
    self.phone_number=phone_number
    self.dob = dob
    self.password = password
    self.name = name
class Response:
  def __init__(self,status,message,user):
    self.status=status
    self.message = message
    self.user=user
class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message

def validateInput(dob,password,name):
  
  def checkInRange(data,fnum,lnum):
    if len(data)>fnum and len(data)<lnum:
      return False
    else:
      return True

  if checkInRange(password,0,10):
    return Validate(status=False,message="password range incorrectly")
  if checkInRange(name,0,10):
    return Validate(status=False,message="name range incorrectly")
  return Validate(status=True,message="no error")
class ApiInfoUser(Resource):
  @jwt_required
  def get(self):
    users = db.selectTable("SELECT * FROM USERS")
    users_info = []
    for i in users:
      id_user,gmail,phone_number,dob,password,name = i
      users_info.append(UserInfo(id_user,gmail,phone_number,dob,password,name).__dict__)
    return Response(
      status=200,
      message="Success",
      user=users_info
    ).__dict__
  @jwt_required
  def put(self):
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      id_user = json_demo["id_user"]
      password = json_demo["password"]
      dob = json_demo['dob']
      name = json_demo['name']
    except:
      id_user = request.form["id_user"]
      password = request.form['password']
      dob = request.form['dob']
      name = request.form['name']
    validate = validateInput( dob=dob, password=password, name=name)
    if validate.status:
      db.updateUser(
        id_user=id_user,
        name=name,
        dob=dob,
        password=password
      )
      print(db.selectTable(("SELECT * from users")))
      return Response(status=201,message=validate.message,user=[]).__dict__
    else:
      return Response(status=400,message=validate.message,user=[]).__dict__
  @jwt_required
  def delete(self):
    id = request.args.get("id")
    print(id)
    db.deleteUser(id)
    return Response(status=200,message=id+"deleted",user=[]).__dict__


    