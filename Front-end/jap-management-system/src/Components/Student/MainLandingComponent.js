import React, { useCallback, useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';

import studentService from '../../Services/studentService';
import selectionService from '../../Services/selectionService';

import StudentActionForms from './StudentActionForms';
import StudentTable from './StudentTable';

import useQuery from '../../Hooks/useQuery';

import classes from '../Style/MainComponent.module.css';
import config from '../../Data/config';

const MainLandingComponent = () => {
  const query = useQuery();
  const [availableSelections, setAvailableSelections] = useState([]);
  const [preSelection, setPreSelection] = useState(null);

  const INITIAL_STUDENT_FORM_DATA = {
    firstName: '',
    lastName: '',
    email: '',
    status: 'InProgram',
    selectionId: null
  };
  const [studentFormData, setStudentFormData] = useState(INITIAL_STUDENT_FORM_DATA);
  const [students, setStudents] = useState([]);
  const INITIAL_SEARCH_STATE = {
    firstName: '',
    lastName: '',
    email: '',
    selectionName: '',
    japProgramName: '',
    status: ''
  };
  const [searchState, setSearchState] = useState(INITIAL_SEARCH_STATE);
  const INITIAL_ACTION_STATE = {
    action: null,
    show: false
  };
  const [actionState, setActionState] = useState(INITIAL_ACTION_STATE);
  const INITIAL_SORT_STATE = {
    sort: 'firstName',
    descending: true
  };
  const [sortState, setSortState] = useState(INITIAL_SORT_STATE);
  const INITIAL_PAGE_STATE = {
    page: 1,
    pageSize: config.PAGE_SIZE
  };
  const [pageState, setPageState] = useState(INITIAL_PAGE_STATE);
  const INITIAL_PAGINATION_INFO_STATE = {
    currentPage: 0,
    pageCount: 0,
    pageSize: 0,
    recordCount: 0
  };
  const [paginationInfo, setPaginationInfoState] = useState(INITIAL_PAGINATION_INFO_STATE);

  const fetchStudents = useCallback(params => {
    studentService.fetchAllStudents(params).then(response => {
      setStudents(response.data.data.results);
      setPaginationInfoState(prevState => {
        if (prevState.recordCount > response.data.data.recordCount && response.data.data.recordCount % 2 === 0) {
          setPageState(prevState => {
            const UPDATED_PAGE_STATE = {
              ...prevState
            };
            UPDATED_PAGE_STATE.page -= 1;
            if (UPDATED_PAGE_STATE.page < 1) {
              UPDATED_PAGE_STATE.page = 1;
            }
            return UPDATED_PAGE_STATE;
          });
        }
        const UPDATED_PAGINATION_INFO_STATE = {
          currentPage: response.data.data.currentPage,
          pageCount: response.data.data.pageCount,
          pageSize: response.data.data.pageSize,
          recordCount: response.data.data.recordCount
        };
        return UPDATED_PAGINATION_INFO_STATE;
      });
    });
  }, []);

  useEffect(() => {
    let search = {
      ...searchState
    };
    const selectionName = query.get('selection');
    if (selectionName) {
      search.selectionName = selectionName;
      selectionService.fetchAllSelections().then(response => {
        let selection = response.data.data.filter(el => el.name === selectionName);
        handleSetPreSelection(selection[0]);
      });
    }

    let params = {
      ...sortState,
      ...pageState,
      ...search
    };
    fetchStudents(params);
  }, [query, pageState, sortState, searchState, fetchStudents]);

  const handleSetPreSelection = selection => {
    setPreSelection(() => selection);
  };

  const handleSortAction = ev => {
    const sort = ev.target.parentNode.getAttribute('name');
    setSortState(previousSort => {
      const UPDATED_SORT = {
        sort,
        descending: sort === previousSort.sort ? !previousSort.descending : true
      };
      return UPDATED_SORT;
    });
  };

  const handlePageState = ev => {
    const action = ev.target.name;
    setPageState(prevState => {
      const UPDATED_PAGE_STATE = {
        page: action === 'prev' ? prevState.page-- : prevState.page++,
        pageSize: +prevState.pageSize
      };
      return UPDATED_PAGE_STATE;
    });
  };

  const handleSearchState = ev => {
    const field = ev.target.name;
    const value = ev.target.value;
    setSearchState(prevState => {
      let UPDATED_SEARCH_STATE = {
        ...prevState
      };
      UPDATED_SEARCH_STATE[field] = value;
      return UPDATED_SEARCH_STATE;
    });
  };

  const handleAddState = () => {
    if (!actionState.show) {
      setStudentFormData(() => {
        return INITIAL_STUDENT_FORM_DATA;
      });
    }
    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'add',
        show: prevState.action === 'add' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleEditState = ev => {
    if (!actionState.show) {
      const id = ev.target.childNodes[0].id;
      const student = students.find(s => s.id === id);
      setStudentFormData(() => {
        return {
          id: student.id,
          firstName: student.firstName,
          lastName: student.lastName,
          email: student.email,
          status: student.status,
          selection: student.selection,
          selectionId: student.selection.id
        };
      });
    }
    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'edit',
        show: prevState.action === 'edit' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleDeleteState = ev => {
    if (!actionState.show) {
      const id = ev.target.childNodes[0].id;
      const student = students.find(s => s.id === id);
      setStudentFormData(() => {
        return {
          id: student.id
        };
      });
    }
    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'delete',
        show: prevState.action === 'delete' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleFetchAvailableSelections = useCallback(() => {
    selectionService
      .fetchAllSelections()
      .then(response => {
        setAvailableSelections(() => response.data.data);
      })
      .catch(err => console.log(err));
  }, []);

  useEffect(() => {
    if ((actionState.action === 'add' || actionState.action === 'edit') && actionState.show)
      handleFetchAvailableSelections();
  }, [actionState, handleFetchAvailableSelections]);

  const handleAddStudent = ev => {
    ev.preventDefault();
    studentService
      .addStudent(studentFormData)
      .then(() => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        let params = {
          ...sortState,
          ...pageState,
          ...searchState
        };
        fetchStudents(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleEditStudent = ev => {
    ev.preventDefault();
    console.log(studentFormData);
    studentService
      .modifyStudent(studentFormData)
      .then(response => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        setStudents(prevState => {
          let student = prevState.findIndex(s => s.id === studentFormData.id);
          prevState[student] = response.data.data;
          return prevState;
        });
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleDeleteStudent = ev => {
    ev.preventDefault();
    studentService
      .deleteStudent(studentFormData)
      .then(() => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        let params = {
          ...sortState,
          ...pageState,
          ...searchState
        };
        fetchStudents(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleStudentFormInput = ev => {
    const inputName = ev.target.name;
    const value = ev.target.value;
    setStudentFormData(prevState => {
      let student = {
        ...prevState
      };
      if (inputName === 'firstName') {
        student.firstName = value;
      } else if (inputName === 'lastName') {
        student.lastName = value;
      } else if (inputName === 'email') {
        student.email = value;
      } else if (inputName === 'status') {
        student.status = value;
      } else if (inputName === 'selection') {
        student.selectionId = value;
      }
      return student;
    });
  };

  const handleResetFilters = () => {
    setSearchState(INITIAL_SEARCH_STATE);
  };

  return (
    <div className={classes.table__container}>
      <div className={classes.student_table_actions}>
        <Button variant="primary" onClick={handleAddState}>
          Add new student
        </Button>
      </div>
      <StudentActionForms
        formModel="student"
        handleAddState={handleAddState}
        handleEditState={handleEditState}
        handleStudentFormInput={handleStudentFormInput}
        handleDeleteStudent={handleDeleteStudent}
        handleAddStudent={handleAddStudent}
        handleEditStudent={handleEditStudent}
        handleDeleteState={handleDeleteState}
        actionState={actionState}
        studentFormData={studentFormData}
        availableSelections={availableSelections}
        preSelection={preSelection}
      />
      <StudentTable
        handlePageState={handlePageState}
        handleSortAction={handleSortAction}
        handleSearchState={handleSearchState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        handleResetFilters={handleResetFilters}
        students={students}
        paginationInfo={paginationInfo}
        sortState={sortState}
        searchState={searchState}
      />
    </div>
  );
};

export default MainLandingComponent;
