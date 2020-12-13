import React, { Component } from "react";
import styles from "./AdminPage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";
import axios from "axios";
import HistoryItem from "../../components/HistoryItem/HistoryItem";
import Cookies from "universal-cookie";

class AdminPage extends Component {
	state = {
		historyItems: [],
	};

	componentDidMount() {
		this.getHistory();
	}

	getHistory = () => {
		const cookies = new Cookies();

		axios
			.get("https://localhost:44326/api/admin", {
				headers: {
					Authorization: "Bearer " + cookies.get("accessToken"),
				},
			})
			.then(response => {
				console.log(response.data.data);
				this.setState({ historyItems: response.data.data });
			})
			.catch(error => {
				console.error(error);
			});
	};

	render() {
		return (
			<div className={styles.AdminPage}>
				<div className={styles.PageHeader}>
					<div>YOU ARE ON ADMIN PAGE.</div>
					<h5 onClick={this.props.logout}>CLICK HERE TO LOG OUT</h5>
				</div>

				<h1>GLOBAL HISTORY</h1>
				<button onClick={this.getHistory}>REFRESH</button>
				<table className={styles.History}>
					<thead>
						<tr>
							<th>USER</th>
							<th>ID</th>
							<th>DATE</th>
							<th>SIZE</th>
							<th>RESULT</th>
						</tr>
					</thead>

					<tbody>
						{this.state.historyItems.reverse().map((historyItem, index) => (
							<HistoryItem source={historyItem} key={index} />
						))}
					</tbody>
				</table>
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
