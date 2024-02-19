import React, { useState } from "react";
import MyModal from "../../../common/Modal/MyModal";

const ModalOrder = ({ orderDetails, visible, setVisible }) => {
  return (
    <MyModal visible={visible} setVisible={setVisible}>
      <div>
        <h3>Order Details</h3>
        {orderDetails && (
          <div>
            <p>
              <strong>Game Name:</strong> {orderDetails.gameName}
            </p>
            <p>
              <strong>Developer:</strong> {orderDetails.gameDeveloper}
            </p>
            <p>
              <strong>Genre:</strong> {orderDetails.gameGenre}
            </p>
            <p>
              <strong>Price:</strong> ${orderDetails.gamePrice}
            </p>
            <p>
              <strong>User Email:</strong> {orderDetails.email}
            </p>
            <p>
              <strong>User Name:</strong> {orderDetails.name}
            </p>
            <p>
              <strong>Date Created:</strong> {orderDetails.dateCreate}
            </p>
          </div>
        )}
      </div>
    </MyModal>
  );
};

export default ModalOrder;
