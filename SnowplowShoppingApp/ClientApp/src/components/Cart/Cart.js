import React, {useState, useEffect} from "react";
import {useSnackbar} from "notistack";

const Cart = (props) => {
  const {enqueueSnackbar} = useSnackbar();

  const renderItems = () => {
    return props.items.map((item, index) => (
      <div className="alert alert-info" key={index}>
        <button
          type="button"
          className="close"
          aria-label="Close"
          onClick={() => removeItem(item)}
        >
          <span aria-hidden="true">
            <small>&times; remove </small>
          </span>
        </button>
        <b>
          {item.product.productName} ({item.product.category})
        </b>{" "}
        <hr />
        Price: ${item.product.price} | Quantity: {item.quantity} |{" "}
        <b>Total: ${item.product.price * item.quantity}</b>
      </div>
    ));
  };

  const removeItem = (item) => {
    props.removeCartItem(item);
    enqueueSnackbar("Item removed from Cart", {
      variant: "success",
    });
  };

  const renderContent = () => {
    let contents =
      props.items.length === 0 ? (
        <p>
          <em>There are no items in the Cart.</em>
        </p>
      ) : (
        renderItems()
      );

    return contents;
  };

  return (
    <div>
      <h1>Shopping cart</h1>
      {renderContent()}
    </div>
  );
};

export default Cart;
