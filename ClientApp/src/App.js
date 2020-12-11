import React from "react";
import { Route, Switch, Redirect } from "react-router";
import "./App.css";
import { connect } from "react-redux";
import * as actionTypes from "./store/actions/actionTypes";

import LandingPage from "./pages/Landing/LandingPage";
import HomePage from "./pages/Home/HomePage";
import AdminPage from "./pages/Admin/AdminPage";

class App extends React.Component {
	componentDidMount() {
		this.props.initAuthData();
	}

	render() {
		return (
			<React.Fragment>
				<Switch>
					{this.props.isAuthenticated ? (
						this.props.userName === " Admin" ? (
							<React.Fragment>
								<Route exact path="/admin" component={AdminPage} />
								<Redirect to="/admin" />
							</React.Fragment>
						) : (
							<React.Fragment>
								<Route exact path="/home" component={HomePage} />
								<Redirect to="/home" />
							</React.Fragment>
						)
					) : (
						<React.Fragment>
							<Route exact path="/" component={LandingPage} />
							<Redirect to="/" />
						</React.Fragment>
					)}
				</Switch>
			</React.Fragment>
		);
	}
}

const mapStateToProps = state => {
	return {
		isAuthenticated: state.isAuthenticated,
		userName: state.userName,
	};
};

const mapDispatchToProps = dispatch => {
	return {
		setAuthData: authData => {
			dispatch({ type: actionTypes.SET_AUTH_DATA, authData: authData });
		},
		initAuthData: () => {
			dispatch({ type: actionTypes.INIT_AUTH_DATA });
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(App);
