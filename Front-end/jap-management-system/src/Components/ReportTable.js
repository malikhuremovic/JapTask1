import React from 'react';

import searchIcon from '../Assets/searchIcon.png';
import sortIconDesc from '../Assets/sortIconAsc.png';

import classes from './ProgramTable.module.css';

const ReportTable = ({ selections }) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        <caption>Selection(s) report</caption>
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div name="name" className={classes.sortBlock}>
                  <img src={sortIconDesc} alt="sorticon" />{' '}
                </div>{' '}
                &nbsp; <span>Selection Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div name="programName" className={classes.sortBlock}>
                  <img src={sortIconDesc} alt="sortIcon" />{' '}
                </div>{' '}
                &nbsp; <span>Program Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div name="successRate" className={classes.sortBlock}>
                  <img src={sortIconDesc} alt="sortIcon" />{' '}
                </div>{' '}
                &nbsp; <span>Success Rate:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div name="overallSuccessRate" className={classes.sortBlock}>
                  <img src={sortIconDesc} alt="sortIcon" />{' '}
                </div>{' '}
                &nbsp; <span>Success Rate:</span>
              </div>
            </th>
          </tr>
          <tr>
            <th scope="col">
              <img
                className={classes.searchIcon}
                src={searchIcon}
                alt="search"
              />
            </th>
            <th scope="col">
              <input
                type="text"
                name="selectionName"
                className="form-control"
                placeholder="Selection Name:"
              />
            </th>
            <th scope="col">
              <input
                name="programName"
                className="form-control"
                type="text"
                placeholder="Program Name:"
              />
            </th>
            <th scope="col">
              <input
                name="successRate"
                className="form-control"
                type="number"
                placeholder="Selection Success Rate:"
              />
            </th>
            <th scope="col">
              <input
                name="overallSuccessRate"
                className="form-control"
                type="number"
                placeholder="Overall Success Rate:"
              />
            </th>
          </tr>
        </thead>
        <tbody>
          {selections &&
            selections.map((s, index) => {
              return (
                <tr key={s.name}>
                  <th scope="row">{index + 1}</th>
                  <td>{s.name}</td>
                  <td>
                    <strong>NOT FETCHED</strong>
                  </td>
                  <td>{s.successRate}%</td>
                  <td>
                    <strong>NOT FETCHED</strong>
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
