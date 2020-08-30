import os
import keras
from keras.models import model_from_json
from keras import backend as K
import tensorflow as tf
from lib.detector import detect_character
from lib.recognition import identify_character
import cv2
import numpy as np
# import logging

# logger = logging.getLogger('root')
config = tf.ConfigProto(
    device_count={'GPU': 1},
    intra_op_parallelism_threads=1,
    allow_soft_placement=True
)

config.gpu_options.allow_growth = True
config.gpu_options.per_process_gpu_memory_fraction = 0.6

class NeuralNetwork:
  def __init__(self,session, graph):
    self.session = session
    self.graph = graph
    keras.backend.set_session(self.session)
    # the folder in which the model and weights are stored
    # for some reason in a flask app the graph/session needs to be used in the init else it hangs on other threads
    # with self.graph.as_default():
    #   with self.session.as_default():

  def predict(self, numpy_img=None):
    """
    :param file_name: [model_file_name, weights_file_name]
    :return:
    """
    with self.graph.as_default():
      with self.session.as_default():
        with self.session.graph.as_default():
          im_test = numpy_img
          images,graph = detect_character(im_test)
          result = []
          for im in images:
            r,gr = identify_character(im,graph)
            result.append(r)
          print(result)