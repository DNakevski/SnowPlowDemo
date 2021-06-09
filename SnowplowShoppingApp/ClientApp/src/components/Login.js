import React, {useState, useEffect} from "react";
import {useHistory} from "react-router-dom";
import {useSnackbar} from "notistack";
import {login, logout} from "../services/UserService";
import {LOGGED_USER} from "../constants";

const Login = (props) => {
  const history = useHistory();
  const {enqueueSnackbar} = useSnackbar();
  const [formValues, setFormValues] = useState({
    userEmail: "",
    userPass: "",
  });

  const handleChangeEvent = (event) => {
    const {name, value} = event.target;
    let newState = {...formValues};
    newState[name] = value;
    setFormValues(newState);
  };

  const handleUserLogin = (event) => {
    event.preventDefault();
    login(formValues.userEmail, formValues.userPass).then((data) => {
      if (data === null) {
        //invalid login
        enqueueSnackbar("Invalid login", {
          variant: "error",
        });
      } else {
        localStorage.setItem(LOGGED_USER, JSON.stringify(data));
        enqueueSnackbar("Successful login", {
          variant: "success",
        });

        props.setUserLoggedIn(true);
        history.push("/");
      }
    });
  };

  useEffect(() => {
    //logout and clear the user from local storage user is present in local storage at first render
    if (localStorage.getItem(LOGGED_USER) !== null) {
      const user = JSON.parse(localStorage.getItem(LOGGED_USER));
      logout(user.email).then((response) => {
        localStorage.removeItem(LOGGED_USER);
        props.setUserLoggedIn(false);
        enqueueSnackbar("You have been logged out", {
          variant: "success",
        });
      });
    }
  }, []);
  return (
    <div className="container">
      <div className="row">
        <div className="col"></div>
        <div className="col">
          <form>
            <div className="form-group">
              <label htmlFor="exampleInputEmail1">Email address</label>
              <input
                type="email"
                name="userEmail"
                className="form-control"
                aria-describedby="emailHelp"
                placeholder="Enter email"
                value={formValues.userEmail}
                onChange={(event) => handleChangeEvent(event, "userEmail")}
              />
              <small id="emailHelp" className="form-text text-muted">
                We'll never share your email with anyone else.
              </small>
            </div>
            <div className="form-group">
              <label htmlFor="exampleInputPassword1">Password</label>
              <input
                type="password"
                name="userPass"
                className="form-control"
                placeholder="Password"
                value={formValues.userPass}
                onChange={(event) => handleChangeEvent(event, "userPass")}
              />
            </div>
            <button
              type="button"
              className="btn btn-primary"
              onClick={handleUserLogin}
            >
              Login
            </button>
          </form>
        </div>
        <div className="col"></div>
      </div>
    </div>
  );
};

export default Login;
