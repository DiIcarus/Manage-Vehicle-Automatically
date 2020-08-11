from flask import Flask
from flask_cors import CORS

app = Flask(__name__)
CORS(app,cross_origin=True,resources={r"/*": {"origins": "*"}},supports_credentials=True)
app.run(port=5000)

@app.route("/")
def helloWorld():
  return "Hello, cross-origin-world!"