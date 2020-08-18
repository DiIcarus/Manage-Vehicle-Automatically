##CONVERT Image
import base64
import cv2
import numpy as np

def numpyImg2Bit64(numpyimg):
    return base64.b64encode(cv2.imencode('.jpg', numpyimg)[1]).decode()

def bit642NumpyImg(encode_string):
    jpg_original = base64.b64decode(encode_string)
    jpg_as_np = np.frombuffer(jpg_original, dtype=np.uint8)
    return cv2.imdecode(jpg_as_np, flags=1)

def img2Bit64(imgpath):
    """Convert image into binary

    Params: 
        imgpath: str
    """
    with open(imgpath, "rb") as original_file:
        encoded_string = base64.b64encode(original_file.read())
    return encoded_string.decode()
    
def bit642Img(encoded_string,destpath):
    """Convert binary into image
    Params:
        encoded_string: str
        destpath: str
    """
    # encode = encoded_string.encode()
    with open(destpath, "wb") as new_file:
        new_file.write(base64.decodebytes(encoded_string))
