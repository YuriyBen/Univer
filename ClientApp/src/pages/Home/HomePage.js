import React, { Component } from "react";
import styles from "./HomePage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";

class HomePage extends Component {
	render() {
		return (
			<div className={styles.HomePage}>
				<h1>Sup, OG, let's multiple some matrixes!</h1>
			</div>
		);
	}
}

const mapStateToProps = state => {
	return {
		userName: state.userName,
		isAuthenticated: state.isAuthenticated,
	};
};

const mapDispatchToProps = dispatch => {
	return {
		logout: () => {
			dispatch({ type: actionTypes.LOG_OUT });
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(HomePage);
