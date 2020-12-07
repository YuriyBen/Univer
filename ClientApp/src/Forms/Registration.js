import React, { Component } from "react";
import axios from "axios";
import styles from "./Form.module.css";

export default class Registration extends Component {
	state = {
		fisrtName: "",
		lastName: "",
		email: "",
		password: "",
		loading: false,
	};

	register = () => {
		this.setState({ loading: true });
		console.log(this.state);
		axios
			.post("https://localhost:44326/api/register", {
				firstName: this.state.firstName,
				lastName: this.state.lastName,
				email: this.state.login,
				password: this.state.password,
			})
			.then(result => {
				console.log("success");
				this.setState({ loading: false });
				this.props.toggle();
			})
			.catch(error => {
				this.setState({ loading: false });
				console.error(error);
			});
	};

	isButtonDisabled = () => {
		return (
			this.state.fisrtName === "" ||
			this.state.lastName === "" ||
			this.state.email === "" ||
			this.state.password === "" ||
			this.state.loading
		);
	};

	render() {
		return (
			<div className={styles.Form}>
				<h1>Registration</h1>
				{this.state.loading ? <label>Loading...</label> : null}
				<br />
				<input
					placeholder="First name"
					type="text"
					onChange={event => {
						this.setState({ fisrtName: event.target.value });
					}}
				/>
				<br />
				<input
					placeholder="Last name"
					type="text"
					onChange={event => {
						this.setState({ lastName: event.target.value });
					}}
				/>
				<br />
				<input
					placeholder="E-mail"
					type="text"
					onChange={event => {
						this.setState({ email: event.target.value });
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
				<button disabled={this.isButtonDisabled()} onClick={this.register}>
					OK
				</button>
				<p onClick={this.props.toggle}>Already have an account? Log in</p>
			</div>
		);
	}
}
