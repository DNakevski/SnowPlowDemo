import {BASE_URL} from "../constants";

export async function login(email, password) {
  const response = await fetch(BASE_URL + "/api/users/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json; charset=UTF-8",
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    body: JSON.stringify({email: email, password: password}),
  });
  let result = await response;
  if (result.status === 400) return null;
  else return result.json();
}
