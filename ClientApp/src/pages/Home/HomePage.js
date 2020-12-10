import React, { Component } from "react";
import styles from "./HomePage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";
import Matrix from "../../components/Matrix/Matrix";
import axios from "axios";

class HomePage extends Component {
	state = {
		matrix1R: "",
		matrix1C: "",
		matrix2C: "",
		matrix2R: "",
		rangeMin: "",
		rangeMax: "",
		enabledGenerate: false,
		enacleCalculate: false,
		firstMatrix: [],
		secondMatrix: [],
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

		this.setState({ firstMatrix, secondMatrix });
	};

	claculate = () => {
		const matrix = {
			matrix1: this.state.matrix1,
			matrix2: this.state.matrix2,
		};
		axios
			.post("/", matrix)
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
				<div>WELCOME, {this.props.userName}, WANNA MULYIPLY SOME MATRIXES?</div>
				<button onClick={this.props.logout}>LOG OUT</button>
				<div className={styles.Range}>
					<div>RANGE OF MATRIX ELEMENTS VALUES</div>
					<input
						placeholder="from"
						type="number"
						value={this.state.rangeMin}
						onChange={event => {
							this.setState({ rangeMin: event.target.value }, this.enabledC);
						}}
					/>
					<input
						placeholder="to"
						type="number"
						value={this.state.rangeMax}
						onChange={event => {
							this.setState({ rangeMax: event.target.value }, this.enabledC);
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
						<button disabled={!this.state.enabledCalculate} style={{ margin: "15px" }}>
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
								this.setState({ matrix1R: event.target.value }, this.enabledC);
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
								this.setState({ matrix2C: event.target.value }, this.enabledC);
							}}
						/>
						<input placeholder="row" type="number" value={this.state.matrix1C} onChange={() => {}} />
					</div>
					<Matrix source={this.state.secondMatrix} />{" "}
				</div>
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
