import React from "react";
import styles from "./Matrix.module.css";

export default function Matrix(props) {
	return (
		<div className={styles.Matrix}>
			{props.source
				? props.source.slice(0, 31).map((row, rowIndex) => {
						return (
							<div key={rowIndex} className={styles.Row}>
								{row.slice(0, 31).map((element, elementIndex) => {
									return (
										<div key={elementIndex} className={styles.Element}>
											{element}
										</div>
									);
								})}
								{row.length > 30 ? <div>...</div> : null}
							</div>
						);
				  })
				: "empty"}
			{props.source.length > 30 ? <div>...</div> : null}
		</div>
	);
}
