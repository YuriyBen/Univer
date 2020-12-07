import React, { Component } from "react";
import styles from "./Login.module.css";
import axios from "axios";
import { connect } from "react-redux";
import * as actionTypes from "../store/actions/actionTypes";

class Login extends Component {
	state = {
		password: "",
		login: "",
		loading: false,
	};

	login = () => {
		console.log(this.state);
		axios
			.post("https://localhost:44326/api/login", { email: this.state.login, password: this.state.password })
			.then(result => {
				this.props.setAuthData(result.data.data);
			})
			.catch(error => {
				console.error(error);
			});
	};

	render() {
		return (
			<div className={styles.Login}>
				<h1>Login</h1>
				<input
					placeholder="E-mail"
					type="text"
					onChange={event => {
						this.setState({ login: event.target.value });
					}}
				/>
				<br />
				<input
					placeholder="Password"
					type="password"
					onChange={event => {
						this.setState({ password: event.target.value });
					}}
				/>
				<br />
				<button onClick={this.login}>OK</button>
				<p onClick={this.props.toggle}>Create an account</p>
			</div>
		);
	}
}

const mapDispatchToProps = dispatch => {
	return {
		setAuthData: authData => {
			dispatch({ type: actionTypes.SET_AUTH_DATA, authData: authData });
		},
	};
};

export default connect(null, mapDispatchToProps)(Login);
