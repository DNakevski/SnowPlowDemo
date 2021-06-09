import React, {useState, useEffect} from "react";
import {getAllProducts} from "../../services/ProductService";
import ProductItem from "./ProductItem";

const ProductsList = (props) => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  const fetchProducts = () => {
    getAllProducts().then((products) => {
      setProducts(products);
      setLoading(false);
    });
  };

  const renderProducts = () => {
    return (
      <div className="container">
        {products.map((product) => (
          <ProductItem
            key={product.productId}
            product={product}
            addToCart={props.addToCart}
          />
        ))}
      </div>
    );
  };

  const renderContent = () => {
    let contents = loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      renderProducts()
    );

    return contents;
  };
  useEffect(() => {
    fetchProducts();
  }, []);

  return (
    <div>
      <h1>Products list</h1>
      {renderContent()}
    </div>
  );
};

export default ProductsList;
