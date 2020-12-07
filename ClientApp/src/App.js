import React from "react";
import { Route, Switch, Redirect } from "react-router";
import "./App.css";
import { connect } from "react-redux";
import * as actionTypes from "./store/actions/actionTypes";
import LandingPage from "./pages/Landing/LandingPage";

class App extends React.Component {
	componentWillMount() {
		this.props.initAuthData();
	}

	render() {
		return (
			<React.Fragment>
				<Switch>
					<Route exact path="/" component={LandingPage} />
					<Route exact path="/qwe" component={null} />
					<Redirect to="/" />
				</Switch>
			</React.Fragment>
		);
	}
}

const mapStateToProps = state => {
	return {
		isAuthenticated: state.isAuthenticated,
	};
};

const mapDispatchToProps = dispatch => {
	return {
		setUserData: authData => {
			dispatch({ type: actionTypes.SET_USER_DATA, authData: authData });
		},
		setAuthData: authData => {
			dispatch({ type: actionTypes.SET_AUTH_DATA, authData: authData });
		},
		initAuthData: () => {
			dispatch({ type: actionTypes.INIT_AUTH_DATA });
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(App);
