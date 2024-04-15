export const getPageCount = (totalCount, gamesPerPage) => {
  return Math.ceil(totalCount / gamesPerPage);
};
