import {BASE_URL} from "../constants";

export async function login(email, password) {
  const response = await fetch(BASE_URL + "/api/users/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json; charset=UTF-8",
    },
    body: JSON.stringify({email: email, password: password}),
  });
  let result = await response;
  if (result.status === 400) return null;
  else return result.json();
}

export async function logout(email) {
  const response = await fetch(BASE_URL + "/api/users/logout", {
    method: "POST",
    headers: {
      "Content-Type": "application/json; charset=UTF-8",
    },
    body: JSON.stringify({email: email}),
  });
  return await response;
}
