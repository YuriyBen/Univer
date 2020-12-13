import React from "react";
import styles from "./Matrix.module.css";

export default function Matrix(props) {
	// const Dots = (len) => {
	// 	return !len > 100;
	// };

	return (
		<div className={styles.Matrix}>
			{props.source
				? props.source.slice(0, 101).map((row, rowIndex) => {
						return (
							<div key={rowIndex} className={styles.Row}>
								{row
									.slice(0, 101)
									.map((element, elementIndex) => {
										return (
											<div
												key={elementIndex}
												className={styles.Element}>
												{element}
											</div>
										);
									})}
								{row.length > 100 ? <div>...</div> : null}
							</div>
						);
				  })
				: "empty"}
			{props.source.length > 100 ? <div>...</div> : null}
		</div>
	);
}
