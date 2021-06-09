import {BASE_URL, LOGGED_USER} from "../constants";

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

export function getLoggedInUser() {
  let loggedInUser = localStorage.getItem(LOGGED_USER);
  if (loggedInUser === null) return null;

  return JSON.parse(loggedInUser);
}
