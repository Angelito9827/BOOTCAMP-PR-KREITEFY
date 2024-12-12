export interface PaginatedResponse<HistoryProfileDto>{
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
    items: HistoryProfileDto [];
}

export interface HistoryProfileDto {
    playedAt: string;
    playedAtFormatted: string;
    songId: number;
    songName: string;
}