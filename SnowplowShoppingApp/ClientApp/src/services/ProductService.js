const BASE_URL = "https://localhost:44312";

export async function getAllProducts() {
  const response = await fetch(BASE_URL + "/api/products");
  return await response.json();
}
