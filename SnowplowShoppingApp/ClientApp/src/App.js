import React, {Component} from "react";
import {Route} from "react-router";
import {Layout} from "./components/Layout";
import Cart from "./components/Cart/Cart";
import ProductsList from "./components/Product/ProductsList";
import Login from "./components/Login";
import {PrivateRoute} from "./hoc/PrivateRoute";
import {getLoggedInUser} from "./services/UserService";
import {
  getCartItemsForUser,
  addToCart,
  removeFromCart,
} from "./services/OrderService";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = {
      cartItems: [],
      isUserLoggedIn: false,
    };
  }

  componentDidMount() {
    const user = getLoggedInUser();
    //get all cart items

    if (user !== null) {
      getCartItemsForUser(user.userId).then((items) => {
        let cartItems = [];
        items.map((item) => {
          cartItems.push({
            userId: item.userId,
            product: item.product,
            quantity: item.quantity,
          });
        });

        localStorage.setItem("cart-items", cartItems.length);
        this.setState({cartItems: cartItems});
      });
    }
  }

  addProductToCart = (product, quantity) => {
    quantity = parseInt(quantity);
    const user = getLoggedInUser();
    const tmpCartItems = this.state.cartItems;
    const newCartItem = {
      userId: user.userId,
      product: product,
      quantity: quantity,
    };

    addToCart(user.userId, product.productId, quantity).then((response) => {
      tmpCartItems.push(newCartItem);
      localStorage.setItem("cart-items", tmpCartItems.length);
      this.setState({cartItems: tmpCartItems});
    });
  };

  removeCartItem = (item) => {
    removeFromCart(item.userId, item.product.productId).then((response) => {
      const tmpCartItems = this.state.cartItems.filter((x) => x !== item);
      localStorage.setItem("cart-items", tmpCartItems.length);
      this.setState({cartItems: tmpCartItems});
    });
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
            <ProductsList {...props} addToCart={this.addProductToCart} />
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
