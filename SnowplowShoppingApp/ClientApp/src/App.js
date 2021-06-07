import React, {Component} from "react";
import {Route} from "react-router";
import {Layout} from "./components/Layout";
import Cart from "./components/Cart/Cart";
import ProductsList from "./components/Product/ProductsList";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    let cartItems = localStorage.getItem("cart-items");
    if (cartItems === null) {
      cartItems = [];
    } else {
      cartItems = JSON.parse(cartItems); //convert them from JSON to array
    }

    super(props);
    this.state = {
      cartItems: cartItems,
    };
  }

  addToCart = (product, quantity) => {
    const tmpCartItems = this.state.cartItems;
    const newCartItem = {
      product: product,
      quantity: quantity,
    };
    tmpCartItems.push(newCartItem);
    localStorage.setItem("cart-items", JSON.stringify(tmpCartItems));
    this.setState({cartItems: tmpCartItems});
  };

  removeCartItem = (item) => {
    const tmpCartItems = this.state.cartItems.filter((x) => x !== item);
    localStorage.setItem("cart-items", JSON.stringify(tmpCartItems));
    this.setState({cartItems: tmpCartItems});
  };

  render() {
    return (
      <Layout>
        <Route exact path="/">
          <ProductsList addToCart={this.addToCart} />
        </Route>
        <Route path="/cart">
          <Cart
            items={this.state.cartItems}
            removeCartItem={this.removeCartItem}
          />
        </Route>
      </Layout>
    );
  }
}
