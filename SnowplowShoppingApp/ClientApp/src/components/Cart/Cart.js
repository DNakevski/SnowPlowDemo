import React from "react";
import {useSnackbar} from "notistack";
import {makeOrder} from "../../services/OrderService";
import {getLoggedInUser} from "../../services/UserService";

const Cart = (props) => {
  const {enqueueSnackbar} = useSnackbar();

  const handleOrderPlaced = () => {
    const user = getLoggedInUser();
    makeOrder(user.userId).then((response) => {
      enqueueSnackbar("Order has been successfully made", {
        variant: "success",
      });
      props.orderPlaced();
    });
  };

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
        <div>
          {renderItems()}
          <div className="d-flex flex-row-reverse">
            <div className="p-2">
              <button
                type="button"
                className="btn btn-primary"
                onClick={handleOrderPlaced}
              >
                Place the order
              </button>
            </div>
          </div>
        </div>
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
