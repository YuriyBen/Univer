import React, { Component } from "react";
import axios from "axios";
import styles from "./Registration.module.css";

export default class Registration extends Component {
	state = {
		fisrtName: "",
		lastName: "",
		email: "",
		password: "",
	};

	register = () => {
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
				this.props.toggle();
			})
			.catch(error => {
				console.error(error);
			});
	};

	render() {
		return (
			<div className={styles.Registration}>
				<h1>Registration</h1>
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
				<button
					onClick={() => {
						console.log(this.state);
					}}
				>
					OK
				</button>
				<p onClick={this.props.toggle}>Already have an account? Log in</p>
			</div>
		);
	}
}
