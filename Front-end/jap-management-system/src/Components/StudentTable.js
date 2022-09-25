import React from 'react';

import searchIcon from '../Assets/searchIcon.png';
import sortIconAsc from '../Assets/sortIconDesc.png';
import sortIconDesc from '../Assets/sortIconAsc.png';

import { Button, Form } from 'react-bootstrap';

import classes from './StudentTable.module.css';
import { Link } from 'react-router-dom';

const StudentTable = ({
  handlePageState,
  handleSortAction,
  handleSearchState,
  handleEditState,
  handleDeleteState,
  handleResetFilters,
  students,
  paginationInfo,
  sortState,
  searchState
}) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        <caption>List of students</caption>
        <caption className={classes.pageButtons}>
          <br />
          {paginationInfo.currentPage > 1 && (
            <React.Fragment>
              <Button name="prev" variant="primary" onClick={handlePageState}>
                &larr;&nbsp; Previous Page
              </Button>
              <span>&nbsp; &nbsp; &nbsp;</span>
            </React.Fragment>
          )}

          {paginationInfo.currentPage < paginationInfo.pageCount && (
            <Button name="next" variant="primary" onClick={handlePageState}>
              Next Page &nbsp;&rarr;
            </Button>
          )}
          <div>
            <br />
            <span>{`Page ${paginationInfo.currentPage} of ${paginationInfo.pageCount}`}</span>
          </div>
        </caption>
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="firstName"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'firstName' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'firstName' &&
                          !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>First Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="lastName"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'lastName' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'lastName' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Last Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="email"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'email' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'email' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Email:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="selection"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'selection' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'selection' &&
                          !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Selection:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="program"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'program' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'program' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Program:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="status"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'status' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'status' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Status:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>Actions: </div>
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
                name="firstName"
                className="form-control"
                placeholder="First name:"
                value={searchState.firstName}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="lastName"
                className="form-control"
                type="text"
                placeholder="Last name:"
                value={searchState.lastName}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="email"
                className="form-control"
                type="text"
                placeholder="Email:"
                value={searchState.email}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="selectionName"
                className="form-control"
                type="text"
                placeholder="Selection:"
                value={searchState.selectionName}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="japProgramName"
                className="form-control"
                type="text"
                placeholder="Program:"
                value={searchState.japProgramName}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <Form.Select
                name="status"
                className="form-select"
                defaultValue=""
                aria-label="Default select example"
                value={searchState.status}
                onChange={handleSearchState}
              >
                <option value="">Select Status</option>
                <option value="InProgram">InProgram</option>
                <option value="Success">Success</option>
                <option value="Failed">Failed</option>
                <option value="Extended">Extended</option>
              </Form.Select>
            </th>
            <th scope="col">
              <Button
                style={{ maxWidth: 120 }}
                name="resetFilters"
                className="form-control"
                type="text"
                onClick={handleResetFilters}
              >
                Reset Filters
              </Button>
            </th>
          </tr>
        </thead>
        <tbody>
          {students.map((s, index) => {
            return (
              <tr key={s.id}>
                <th scope="row">{index + 1}</th>
                <td>{s.firstName}</td>
                <td>{s.lastName}</td>
                <td>{s.email}</td>
                <td>{s.selection.name}</td>
                <td>{s.selection.japProgram.name}</td>
                <td>{s.status}</td>
                <td>
                  <Link to={`/student?id=${s.id}`}>
                    <Button
                      className={classes.action__button}
                      variant="primary"
                    >
                      Details
                    </Button>
                  </Link>
                  <Button
                    className={classes.action__button}
                    variant="success"
                    onClick={handleEditState}
                  >
                    <input id={s.id} type="hidden" />
                    Edit
                  </Button>
                  <Button
                    className={classes.action__button}
                    variant="danger"
                    onClick={handleDeleteState}
                  >
                    <input id={s.id} type="hidden" />
                    Delete
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

export default StudentTable;
