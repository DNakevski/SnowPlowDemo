import React, {Component} from "react";
import {Route} from "react-router";
import {Layout} from "./components/Layout";
import Cart from "./components/Cart/Cart";
import ProductsList from "./components/Product/ProductsList";
import Login from "./components/Login";
import {PrivateRoute} from "./hoc/PrivateRoute";

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
      isUserLoggedIn: false,
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

  setUserLoggedIn = (isLoggedIn) => {
    this.setState({isUserLoggedIn: isLoggedIn});
  };

  render() {
    return (
      <Layout>
        <PrivateRoute
          exact={true}
          path="/"
          render={(props) => (
            <ProductsList {...props} addToCart={this.addToCart} />
          )}
        />
        <PrivateRoute
          exact={false}
          path="/cart"
          render={(props) => (
            <Cart
              items={this.state.cartItems}
              removeCartItem={this.removeCartItem}
            />
          )}
        />
        <Route path="/login">
          <Login setUserLoggedIn={this.setUserLoggedIn} />
        </Route>
      </Layout>
    );
  }
}
