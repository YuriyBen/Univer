import React, { Component } from "react";
import axios from "axios";
import styles from "./Form.module.css";

export default class Registration extends Component {
	state = {
		firstName: "",
		lastName: "",
		phone: "",
		password: "",
		loading: false,
		errorMessage: "",
	};

	register = () => {
		this.setState({ loading: true });
		axios
			.post("https://localhost:44326/api/register", {
				firstName: this.state.firstName,
				lastName: this.state.lastName,
				phone: this.state.email,
				password: this.state.password,
			})
			.then(result => {
				this.setState({ loading: false });
				if (result.data.status === 0) {
					this.props.toggle();
				} else {
					this.setState({ errorMessage: result.data.data });
				}
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
						this.setState({ firstName: event.target.value });
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
					placeholder="Phone number"
					defaultValue="+380"
					type="phone"
					onChange={event => {
						this.setState({ phone: event.target.value });
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
				<label className={styles.errorMessage}>{this.state.errorMessage}</label>
				<br />
				<button disabled={this.isButtonDisabled()} onClick={this.register}>
					OK
				</button>
				<p onClick={this.props.toggle}>Already have an account? Log in</p>
			</div>
		);
	}
}
