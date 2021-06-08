import {BASE_URL} from "../constants";

export async function getAllProducts() {
  const response = await fetch(BASE_URL + "/api/products");
  return await response.json();
}
