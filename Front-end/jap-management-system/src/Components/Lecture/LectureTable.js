import React from 'react';
import { Button, Form, Row, Col } from 'react-bootstrap';

import searchIcon from '../../Assets/searchIcon.png';
import sortIconAsc from '../../Assets/sortIconDesc.png';
import sortIconDesc from '../../Assets/sortIconAsc.png';

import classes from '../Style/Table.module.css';

const LectureTable = ({
  handlePageState,
  handleSortAction,
  handleSearchState,
  handleEditState,
  handleDeleteState,
  handleResetFilters,
  lectures,
  paginationInfo,
  sortState,
  searchState
}) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        <caption>List of lectures</caption>
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
                  name="name"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'name' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'name' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="description"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'description' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'description' &&
                          !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Description:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="URL"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'URL' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'URL' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>URL:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="expectedHours"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'expectedHours' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'expectedHours' &&
                          !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Expected hours:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="isEvent"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'isEvent' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'isEvent' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Event:</span>
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
                name="name"
                className="form-control"
                placeholder="Name:"
                value={searchState.name}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="description"
                className="form-control"
                type="text"
                placeholder="Description:"
                value={searchState.description}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="url"
                className="form-control"
                type="text"
                placeholder="URL:"
                value={searchState.URL}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="expectedHours"
                className="form-control"
                type="number"
                placeholder="Expected hours:"
                value={searchState.expectedHours}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <Form.Group
                as={Row}
                className="mb-3"
                controlId="formHorizontalFirstName"
              >
                <Col sm={10}>
                  <Form.Check
                    className={classes.checboxFilter}
                    name="isEvent"
                    label="Filter by event property"
                    value={searchState?.isEvent ? false : true}
                    onChange={handleSearchState}
                    checked={searchState?.isEvent === true}
                  />
                </Col>
              </Form.Group>
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
          {lectures &&
            lectures.map((s, index) => {
              return (
                <tr key={s.id}>
                  <th scope="row">
                    <Button style={{ minWidth: 40 }} variant="success" disabled>
                      <span style={{ fontSize: 16 }}>
                        {index +
                          1 +
                          (paginationInfo.currentPage > 1
                            ? paginationInfo.currentPage *
                                paginationInfo.pageSize -
                              paginationInfo.pageSize
                            : 0)}
                      </span>
                    </Button>
                  </th>
                  <td>{s.name}</td>
                  <td>{s.description}</td>
                  <td>
                    {s.url.split(',').map(url => {
                      return (
                        <a
                          style={{ textDecoration: 'none', marginRight: 10 }}
                          href={url.trim()}
                        >
                          <Button variant="outline-primary">Link</Button>
                        </a>
                      );
                    })}
                  </td>
                  <td>
                    {' '}
                    <Button
                      style={{ minWidth: 50 }}
                      disabled
                      variant="outline-success"
                    >
                      {s.expectedHours}h
                    </Button>
                  </td>
                  <td>
                    {!s.isEvent ? (
                      <Button
                        style={{ minWidth: 100 }}
                        variant="warning"
                        disabled
                      >
                        <strong>Lecture</strong>
                      </Button>
                    ) : (
                      <Button
                        style={{ minWidth: 100 }}
                        variant="danger"
                        disabled
                      >
                        <strong>Event</strong>
                      </Button>
                    )}
                  </td>
                  <td>
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

export default LectureTable;
