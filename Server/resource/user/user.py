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
  def post(self):
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      gmail = json_demo["gmail"]
      password = json_demo["password"]
    except:
      gmail = request.form['gmail']
      password = request.form['password']
    