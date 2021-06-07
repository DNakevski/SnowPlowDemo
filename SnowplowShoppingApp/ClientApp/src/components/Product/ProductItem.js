import React, {useState} from "react";
import {useSnackbar} from "notistack";

const ProductItem = (props) => {
  const {enqueueSnackbar} = useSnackbar();

  const product = props.product;
  const [quantity, setQuantity] = useState(1);

  const handleQuantityChange = (event) => {
    const qty = event.target.value;
    setQuantity(qty);
  };

  const handleAddToCardClick = (event) => {
    event.preventDefault();
    props.addToCart(product, quantity);
    enqueueSnackbar("Item added to Cart", {
      variant: "success",
    });
  };

  return (
    <div className="row">
      <div className="col-sm">
        <div className="jumbotron">
          <h1 className="display-6">
            {product.productName} (${product.price})
          </h1>
          <p className="lead">
            {product.description} - ({product.category})
          </p>
          <hr className="my-4" />
          <form className="form-inline">
            <div className="form-group">
              <label htmlFor="quantityTxt" className="col-sm-1 col-form-label">
                Quantity:
              </label>
            </div>
            <div className="form-group">
              <input
                type="text"
                className="form-control-text"
                id="quantityTxt"
                value={quantity}
                onChange={handleQuantityChange}
              />
            </div>
            <button
              type="button"
              className="btn btn-primary mb-2 ml-2"
              onClick={handleAddToCardClick}
            >
              Add to cart
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ProductItem;
