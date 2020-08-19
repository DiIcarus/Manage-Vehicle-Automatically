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
def  validatePassword(gmail, password):
  '''
    sign in using gmail and passwork.
    return true when data is available
  '''
  return True

class ApiSignIn(Resource):
  def post(self):
    gmail = request.form['gmail']
    password = request.form['password']
    obj = {
      'user':"son",
      'pws':"123"
    }
    if validatePassword( gmail=gmail, password=password):
      access_token = create_access_token(identity = obj)
      ret = {
        'access_token': access_token,
        }
      return ret
    return 200

  @jwt_required
  def get(self):
    return {
      'hello_is': 'asd',
      'foo_is': 'asd'
    }, 200
