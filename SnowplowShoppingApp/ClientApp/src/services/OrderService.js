import {BASE_URL} from "../constants";

export async function getCartItemsForUser(userId) {
  const response = await fetch(BASE_URL + "/api/orders/cartitems/" + userId);
  return await response.json();
}

export async function addToCart(userId, productId, quantity) {
  const response = await fetch(BASE_URL + "/api/orders/cartitems", {
    method: "POST",
    headers: {
      "Content-Type": "application/json; charset=UTF-8",
    },
    body: JSON.stringify({
      userId: userId,
      productId: productId,
      quantity: quantity,
    }),
  });
  return response;
}

export async function removeFromCart(userId, productId) {
  const response = await fetch(
    BASE_URL +
      "/api/orders/cartitems?userId=" +
      userId +
      "&productId=" +
      productId,
    {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json; charset=UTF-8",
      },
    }
  );
  return await response;
}

export async function makeOrder(userId) {
  const response = await fetch(BASE_URL + "/api/orders/makeorder", {
    method: "POST",
    headers: {
      "Content-Type": "application/json; charset=UTF-8",
    },
    body: JSON.stringify({userId: userId}),
  });
  return await response;
}
