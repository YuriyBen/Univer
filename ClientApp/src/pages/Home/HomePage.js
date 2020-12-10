import React, { Component } from "react";
import styles from "./HomePage.module.css";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";
import axios from "axios";

class HomePage extends Component {
	state = {
		matrix1R: null,
		matrix1C: null,
		matrix2C: null,
		matrix2R: null,
		rangeMin: null,
		rangeMax: null,
		enabledGenerate: false,
	};

	enabledC = () => {
		if (
			(this.state.matrix1C &&
				this.state.matrix1R &&
				this.state.matrix2C &&
				this.state.matrix2R &&
				this.state.rangeMin &&
				this.state.rangeMax) !== null
		) {
			this.setState({
				enabledGenerate: true,
				matrix2R: this.state.matrix1C,
			});
		}
		console.log(this.state);
	};

	generate = () => {};

	// claculate = () => {
	// 	axios
	// 		.get("/" + )
	// 		.then((response) => {
	// 			this.setState({
	// 				currentAction: response.data,
	// 			});
	// 		})

	// 		.catch((error: any) => {
	// 			console.log(error);
	// 		});
	// };

	render() {
		return (
			<div className={styles.HomePage}>
				<div>
					<div
						style={{
							display: "inline",
							float: "left",
							margin: "20px",
						}}>
						<div>first matrix</div>
						<input
							placeholder="column"
							type="number"
							value={this.state.matrix1C}
							onChange={(event) => {
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
							onChange={(event) => {
								this.setState(
									{ matrix1R: event.target.value },
									this.enabledC
								);
							}}
						/>
					</div>
					<div
						style={{
							display: "inline",
							float: "right",
							margin: "20px",
						}}>
						<div>second matrix</div>
						<input
							placeholder="column"
							type="number"
							value={this.state.matrix2C}
							onChange={(event) => {
								this.setState(
									{ matrix2C: event.target.value },
									this.enabledC
								);
							}}
						/>
						<input
							placeholder="row"
							type="number"
							value={this.state.matrix1C}
						/>
					</div>
					<div style={{ height: "100px" }}></div>
					<div className={styles.Range}>
						<div>input range of random digits</div>
						<input
							placeholder="from"
							type="number"
							value={this.state.rangeMin}
							onChange={(event) => {
								this.setState(
									{ rangeMin: event.target.value },
									this.enabledC
								);
							}}
						/>
						<input
							placeholder="to"
							type="number"
							value={this.state.rangeMax}
							onChange={(event) => {
								this.setState(
									{ rangeMax: event.target.value },
									this.enabledC
								);
							}}
						/>
					</div>

					<button
						disabled={!this.state.enabledGenerate}
						style={{ margin: "15px" }}>
						generate
					</button>
					<button
						disabled={!this.state.enabledCalculate}
						style={{ margin: "15px" }}>
						calculate
					</button>
				</div>

				<div>
					<div
						className={styles.Matrix}
						style={{ float: "left", margin: "15px" }}>
						686868686868688686686868686868688686686868686868688686686868686868688686686868686868688686686868686868688686686868686868688686686868686868688686
					</div>
					<div
						className={styles.Matrix}
						style={{ float: "right", margin: "15px" }}>
						6
					</div>
				</div>
			</div>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		userName: state.userName,
		isAuthenticated: state.isAuthenticated,
	};
};

const mapDispatchToProps = (dispatch) => {
	return {
		logout: () => {
			dispatch({ type: actionTypes.LOG_OUT });
		},
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(HomePage);
