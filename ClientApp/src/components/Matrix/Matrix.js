import React from "react";
import styles from "./Matrix.module.css";

export default function Matrix(props) {
	return (
		<div className={styles.Matrix}>
			{props.source
				? props.source.map((row, rowIndex) => {
						return (
							<div key={rowIndex} className={styles.Row}>
								{row.map((element, elementIndex) => {
									return (
										<div key={elementIndex} className={styles.Element}>
											{element}
										</div>
									);
								})}
							</div>
						);
				  })
				: "empty"}
		</div>
	);
}
