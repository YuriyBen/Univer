import React, { Component } from "react";
import styles from "./LandingPage.module.css";
import Login from "../../Login/Login";
import Registration from "../../Registration/Registration";

export default class LandingPage extends Component {
	state = {
		showLogin: true,
	};

	toggle = () => {
		this.setState({ showLogin: !this.state.showLogin });
	};

	render() {
		return (
			<div className={styles.LandingPage}>
				{this.state.showLogin ? <Login toggle={this.toggle} /> : <Registration toggle={this.toggle} />}
			</div>
		);
	}
}
