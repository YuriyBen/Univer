import React, { Component } from "react";
import styles from "./HistoryItem.module.css";

export default function HistoryItem(props) {
	return (
		<tr className={styles.HistoryItem}>
			{props.source.username ? (
				<td headers="userName" className={styles.HistoryItemProp}>
					{props.source.username}
				</td>
			) : null}
			{props.source.id ? (
				<td headers="id" className={styles.HistoryItemProp}>
					{props.source.id}
				</td>
			) : null}
			<td headers="date" className={styles.HistoryItemProp}>
				{props.source.date}
			</td>
			<td headers="size" className={styles.HistoryItemProp}>
				{props.source.matrixSizes}
			</td>
			<td headers="status" className={styles.HistoryItemProp}>
				{props.source.status}
			</td>
			<td headers="result" className={styles.HistoryItemProp}>
				{props.source.result}
			</td>
		</tr>
	);
}
