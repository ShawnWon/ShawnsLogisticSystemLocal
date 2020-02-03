import pandas as pd
import numpy as np
from flask import Flask, request, jsonify
import pickle
import keras
from keras import backend as K
import tensorflow

app = Flask(__name__)


@app.route('/model1', methods=['GET'])
def callModelOne():
    xValue = request.args.get('x')
    df=list(map(int,xValue.split(',')))
    print("Received request to predict demanding quantity.")
    print("The latest 12 months' demanding is ")
    print(df)
    modelOne = pickle.load(open('item1predictormodel.pkl', 'rb')) # load model to the server
    
    
    x1=np.asarray(df,dtype=np.int64)
    x2=x1.reshape((-1,1))
    x2=x2.reshape((-1))
    x3=x2.reshape((1,12,1))
    
    
    pre=modelOne.predict(x3)[0][0]
    print("The prediction result is")
    print(pre)
    K.clear_session()
    return str(int(pre))

# run the server
if __name__ == '__main__':
    app.run(port=5000, debug=True)
