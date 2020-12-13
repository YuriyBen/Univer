import React, { Component } from "react";
import styles from "./HomePage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";
import Matrix from "../../components/Matrix/Matrix";
import axios from "axios";
import cookies from "universal-cookie";
import HistoryItem from "../../components/HistoryItem/HistoryItem";

class HomePage extends Component {
	state = {
		matrix1R: "",
		matrix1C: "",
		matrix2C: "",
		matrix2R: "",
		rangeMin: "",
		rangeMax: "",
		enabledGenerate: false,
		enableCalculate: false,
		firstMatrix: [],
		secondMatrix: [],
		historyItems: [
			{ id: "123", date: "12.11.2020", matrixSizes: "30x40", status: "done", result: "123" },
			{ id: "234", date: "10.12.2020", matrixSizes: "12x45", status: "cancelled", result: "321" },
			{ id: "345", date: "01.10.2020", matrixSizes: "32x4", status: "processing", result: "213" },
		],
	};

	enabledC = () => {
		if (
			this.state.matrix1C !== "" &&
			this.state.matrix1R !== "" &&
			this.state.matrix2C !== "" &&
			this.state.matrix2R !== "" &&
			this.state.rangeMin !== "" &&
			this.state.rangeMax !== ""
		) {
			this.setState({
				enabledGenerate: true,
				matrix2R: this.state.matrix1C,
			});
		} else {
			this.setState({
				enabledGenerate: false,
				matrix2R: this.state.matrix1C,
			});
		}
	};

	generate = () => {
		this.setState({ enableCalculate: false }, () => {
			const firstMatrix = [];
			for (let a = 0; a < this.state.matrix1R; ++a) {
				firstMatrix.push([]);
				for (let b = 0; b < this.state.matrix1C; ++b) {
					firstMatrix[a].push(
						Math.round(Math.random() * (+this.state.rangeMax - +this.state.rangeMin) + +this.state.rangeMin)
					);
				}
			}

			const secondMatrix = [];
			for (let a = 0; a < this.state.matrix2R; ++a) {
				secondMatrix.push([]);
				for (let b = 0; b < this.state.matrix2C; ++b) {
					secondMatrix[a].push(
						Math.round(Math.random() * (+this.state.rangeMax - +this.state.rangeMin) + +this.state.rangeMin)
					);
				}
			}

			this.setState({
				enableCalculate: true,
				firstMatrix: firstMatrix,
				secondMatrix: secondMatrix,
			});
		});
	};

	claculate = () => {
		const Cookies = new cookies();
		axios
			.post(
				"https://localhost:44326/api/math-task",
				{
					userId: this.props.userId,
					matrix1: this.state.firstMatrix,
					matrix2: this.state.secondMatrix,
				},
				{
					headers: {
						Authorization: "Bearer " + Cookies.get("accessToken"),
					},
				}
			)
			.then(response => {
				console.log(response);
			})
			.catch(error => {
				console.log(error);
			});
	};

	render() {
		return (
			<div className={styles.HomePage}>
				<div className={styles.PageHeader}>
					<div>WELCOME, {this.props.userName}, WANNA MULTIPLY SOME MATRIXES?</div>
					<h5 onClick={this.props.logout}>CLICK HERE TO LOG OUT</h5>
				</div>
				<div className={styles.Range}>
					<div>RANGE OF MATRIX ELEMENTS VALUES</div>
					<input
						placeholder="from"
						type="number"
						value={this.state.rangeMin}
						onChange={event => {
							this.setState(
								{
									rangeMin: event.target.value,
									enableCalculate: false,
								},
								this.enabledC
							);
						}}
					/>
					<input
						placeholder="to"
						type="number"
						value={this.state.rangeMax}
						onChange={event => {
							this.setState(
								{
									rangeMax: event.target.value,
									enableCalculate: false,
								},
								this.enabledC
							);
						}}
					/>
					<div>
						<button
							disabled={!this.state.enabledGenerate}
							style={{ margin: "15px" }}
							onClick={this.generate}
						>
							GENERATE
						</button>
						<button
							disabled={!this.state.enableCalculate}
							style={{ margin: "15px" }}
							onClick={this.claculate}
						>
							MULTIPLY
						</button>
					</div>
				</div>
				<div className={styles.MatrixHolder}>
					<div className={styles.InputHolder}>
						<div>FIRST MATRIX</div>
						<input
							placeholder="column"
							type="number"
							value={this.state.matrix1C}
							onChange={event => {
								this.setState(
									{
										matrix1C: event.target.value,
										matrix2R: event.target.value,
										enableCalculate: false,
									},
									this.enabledC
								);
							}}
						/>
						<input
							placeholder="row"
							type="number"
							value={this.state.matrix1R}
							onChange={event => {
								this.setState(
									{
										matrix1R: event.target.value,
										enableCalculate: false,
									},
									this.enabledC
								);
							}}
						/>
					</div>
					<Matrix source={this.state.firstMatrix} />
				</div>
				<div className={styles.MatrixHolder}>
					<div className={styles.InputHolder}>
						<div>SECOND MATRIX</div>
						<input
							placeholder="column"
							type="number"
							value={this.state.matrix2C}
							onChange={event => {
								this.setState(
									{
										matrix2C: event.target.value,
										enableCalculate: false,
									},
									this.enabledC
								);
							}}
						/>
						<input placeholder="row" type="number" value={this.state.matrix1C} onChange={() => {}} />
					</div>
					<Matrix source={this.state.secondMatrix} />{" "}
				</div>
				<h1>HISTORY</h1>
				<table className={styles.History}>
					<thead>
						<tr>
							<th>ID</th>
							<th>DATE</th>
							<th>SIZE</th>
							<th>STATUS</th>
							<th>RESULT</th>
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

export default connect(mapStateToProps, mapDispatchToProps)(HomePage);
