import React from 'react';
import { Button } from 'react-bootstrap';

import searchIcon from '../../Assets/searchIcon.png';

import classes from './ReportTable.module.css';

const ReportTable = ({ selections }) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        {selections.length ? (
          <caption>
            <span className={classes.overall}>
              <Button variant="primary" disabled>
                Overall success rate: {selections[0]?.overallSuccessRate}%
              </Button>
            </span>
          </caption>
        ) : (
          <caption>
            <span className={classes.overall}>
              <Button variant="danger" disabled>
                No statistics could be found
              </Button>
            </span>
          </caption>
        )}
        <caption>
          {selections.length > 1
            ? 'Selection(s)'
            : !selections.length
            ? 'No'
            : selections.length === 1
            ? 'Selection'
            : ''}{' '}
          report
        </caption>
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                &nbsp; <span>Selection Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                &nbsp; <span>Program Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                &nbsp; <span>Success Rate:</span>
              </div>
            </th>
          </tr>
          <tr>
            <th scope="col">
              <img className={classes.searchIcon} src={searchIcon} alt="search" />
            </th>
            <th scope="col">
              <input
                type="text"
                name="selectionName"
                className="form-control"
                placeholder="Selection name: "
                disabled
              />
            </th>
            <th scope="col">
              <input name="programName" className="form-control" type="text" placeholder="Program name: " disabled />
            </th>
            <th scope="col">
              <input name="successRate" className="form-control" type="number" placeholder="Success rate: " disabled />
            </th>
          </tr>
        </thead>
        <tbody>
          {selections &&
            selections.map((s, index) => {
              return (
                <tr key={s.selectionName}>
                  <th scope="row">
                    {' '}
                    <Button style={{ minWidth: 40 }} variant="secondary" disabled>
                      <span style={{ fontSize: 16 }}>{index + 1}</span>
                    </Button>
                  </th>
                  <td>{s.selectionName}</td>
                  <td>{s.programName}</td>
                  <td>
                    <Button variant={s.selectionSuccessRate >= 50 ? 'success' : 'danger'} disabled>
                      {s.selectionSuccessRate}%
                    </Button>
                  </td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default ReportTable;
