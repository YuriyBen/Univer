import React, { Component } from "react";
import styles from "./AdminPage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";
import axios from "axios";
import HistoryItem from "../../components/HistoryItem/HistoryItem";

class AdminPage extends Component {
	state = {
		information: null,
	};

	componentDidMount() {
		this.interval = setInterval(
			() => this.setState({ time: Date.now() }),
			1000
		);
	}

	componentWillUnmount() {
		clearInterval(this.interval);

		axios
			.get("/blablabla")
			.then((response) => {
				this.setState({ information: response.data });
			})
			.catch((error) => {
				console.error(error);
			});
	}

	render() {
		return (
			<div className={styles.AdminPage}>
				<div>YOU ARE ON ADMIN PAGE.</div>
				<button onClick={this.props.logout}>LOG OUT</button>
				<h1>HISTORY</h1>
				<table className={styles.History}>
					<thead>
						<tr>
							<th>UZER NAME</th>
							<th>ID</th>
							<th>DATE</th>
							<th>SIZE</th>
						</tr>
					</thead>

					<tbody>
						{this.state.historyItems.map((historyItem, index) => (
							<HistoryItem source={historyItem} key={index} />
						))}
					</tbody>
				</table>
			</div>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		userName: state.userName,
		isAuthenticated: state.isAuthenticated,
		userId: state.userId,
	};
};

const mapDispatchToProps = (dispatch) => {
	return {
		logout: () => {
			dispatch({ type: actionTypes.LOG_OUT });
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(AdminPage);
