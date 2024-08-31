from flask import Flask, jsonify
import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPRegressor
from sklearn.metrics import mean_squared_error

app = Flask(_name_)

@app.route('/run_model', methods=['POST'])
def run_model():
    # Load the different irradiance datasets
    global_horizontal = pd.read_csv('global_horizontal_means.csv', delimiter=';')
    par = pd.read_csv('par_means.csv', delimiter=';')
    direct_normal = pd.read_csv('direct_normal_means.csv', delimiter=';')
    tilted_latitude = pd.read_csv('tilted_latitude_means.csv', delimiter=';')

    # Merge datasets (assuming they share 'ID', 'LON', and 'LAT' columns)
    merged_data = global_horizontal.merge(par, on=['ID', 'LON', 'LAT'], suffixes=('_global', '_par')).merge(
        direct_normal, on=['ID', 'LON', 'LAT'], suffixes=('', '_direct')
    ).merge(
        tilted_latitude, on=['ID', 'LON', 'LAT'], suffixes=('', '_tilted')
    )

    # Choose target variable (e.g., 'GLOBAL_HORIZONTAL') and features
    X = merged_data[['ANNUAL_global', 'JAN_global', 'FEB_global', 'MAR_global', 'APR_global',
                    'MAY_global', 'JUN_global', 'JUL_global', 'AUG_global', 'SEP_global',
                    'OCT_global', 'NOV_global', 'DEC_global']]  # Global horizontal irradiance features
    y = merged_data['ANNUAL_global']  # Predict annual global horizontal irradiance

    # Split data into training and testing sets
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

    # Standardize features (important for ANN)
    scaler = StandardScaler()
    X_train = scaler.fit_transform(X_train)
    X_test = scaler.transform(X_test)

    # ANN
    ann_model = MLPRegressor(hidden_layer_sizes=(100, 50), activation='relu', solver='adam',
                            max_iter=500, random_state=42)  # Example parameters
    ann_model.fit(X_train, y_train)
    y_pred_ann = ann_model.predict(X_test)
    rmse_ann = mean_squared_error(y_test, y_pred_ann, squared=False)

    return jsonify(rmse_ann)

if _name_ == '_main_':
    app.run(debug=True, port=5000)