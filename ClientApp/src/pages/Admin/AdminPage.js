import React, { Component } from "react";
import styles from "./AdminPage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";

class AdminPage extends Component {
	render() {
		return (
			<div className={styles.AdminPage}>
				<div>YOU ARE ON ADMIN PAGE.</div>
				<button onClick={this.props.logout}>LOG OUT</button>
			</div>
		);
	}
}

const mapStateToProps = state => {
	return {
		userName: state.userName,
		isAuthenticated: state.isAuthenticated,
		userId: state.userId,
	};
};

const mapDispatchToProps = dispatch => {
	return {
		logout: () => {
			dispatch({ type: actionTypes.LOG_OUT });
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(AdminPage);
