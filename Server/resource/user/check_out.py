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
class CheckOutInfo:
  def __init__(self,id_check_in,vehicle_id,key_code,share_code,dates):
    self.id_check_in=id_check_in
    self.vehicle_id=vehicle_id
    self.key_code=key_code
    self.share_code=share_code
    self.dates = dates
class Response:
  def __init__(self,status,message,user):
    self.status=status
    self.message = message
    self.user=user
class ApiInfoCheckOut(Resource):
  @jwt_required
  def get(self):
    check_in = db.selectTable("SELECT * FROM `check-out`")
    checked_info = []
    for i in check_in:
      id_check_in,vehicle_id,key_code,share_code,dates = i
      checked_info.append(CheckOutInfo(id_check_in,vehicle_id,key_code,share_code,dates).__dict__)
    return Response(
      status=200,
      message="Success",
      user=checked_info
    ).__dict__