import React from "react";
import { API_URL } from "../../../common/services/constants/config";
import OrderHandler from "../Utils/OrderHandler";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const OrdersList = ({ orders, onOrderClick }) => {
  const { handleDelete, error } = OrderHandler();

  const onDelete = async (orderId) => {
    try {
      await handleDelete(orderId);
      toast.success("Order deleted successfully!");
    } catch (error) {
      console.error("Error deleting order:", error);
      toast.error("Failed to delete order.");
    }
  };

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
                  <button
                    className="btn btn-primary mb-2"
                    onClick={() => onOrderClick(order.id)}
                  >
                    View Details
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() => onDelete(order.id)}
                  >
                    Delete
                  </button>
                  {error && <p>Error deleting order: {error}</p>}
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
      <ToastContainer />
    </div>
  );
};

export default OrdersList;
