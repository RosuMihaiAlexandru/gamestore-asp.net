import React from "react";
import { API_URL } from "../../../common/services/http/config";

const OrdersList = ({ orders }) => {
  if (orders.length === 0) {
    return <p>Cart is empty :(</p>;
  }

  const totalPrice = orders.reduce((total, order) => {
    // Replace the comma with a dot because the price format is incorrect
    const price = order.gamePrice.replace(",", ".");
    return total + Number(price);
  }, 0);

  return (
    <div className="row">
      <div className="col-md-8">
        {orders.map((order) => (
          <div key={order.id} className="card mb-4">
            <div className="row g-0">
              <div className="col-md-4 mb-3">
                <img
                  src={`${API_URL}${order.imagePath}`}
                  alt={order.gameName}
                  className="img-fluid p-3"
                  style={{ width: "65%" }}
                />
              </div>
              <div className="col-md-5">
                <div className="card-body">
                  <h4 className="card-title mb-4">{order.gameName}</h4>
                  <h5 className="card-title mb-4">{order.gameDeveloper}</h5>
                  <h5 className="card-title ">{order.gameGenre}</h5>
                </div>
              </div>
              <div className="col-md-3 d-flex justify-content-center align-items-center">
                <div className="card-body">
                  <h5 className="card-title ">${order.gamePrice}</h5>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
      <div className="col-md-3">
        <div className="card text-center">
          <div className="card-body d-flex justify-content-between">
            <h5 className="card-title">Total price</h5>
            <h5 className="card-text">${totalPrice.toFixed(2)}</h5>
          </div>
          <div className="p-2">
            <button className="btn btn-primary btn-lg">
              Continue to payment
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default OrdersList;
